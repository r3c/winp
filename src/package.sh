#!/bin/sh -e

# Package configuration
artifact=winp
project=Winp
runtimes='win-x64'

# Create archive for each target runtime
base="$(dirname "$0")"
source="$(mktemp -d)"

for runtime in $runtimes; do
	archive="$artifact-$runtime.zip"

	# Publish archive for requested runtime
	echo >&2 "publishing $archive..."

	dotnet publish --nologo --self-contained --configuration Release --runtime "$runtime" --verbosity quiet "$base/$project"

	# Create temporary directory to get desired archive path
	ln -s "$(realpath "$base/$project/bin/Release/"*"/$runtime/publish")" "$source/$artifact"

	(cd "$source" && zip -qr "$archive" "$artifact")

	# Move and erase temporary directory
	mv "$source/$archive" "$base/$archive"
	rm "$source/$artifact"
done

echo >&2 "done."

# Cleanup
rm -r "$source"
