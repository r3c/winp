#!/bin/sh -e

# Package configuration
artifact=winp
framework=netcoreapp3.1
project=Winp
runtimes=win-x64

# Retreive latest version tag from current HEAD
base="$(dirname "$0")"
tag="$(git --work-tree "$base" tag --points-at HEAD)"

if [ -n "$tag" ]; then
	version="v$tag"
else
	echo >&2 "warning: missing tag on HEAD, will assume draft version"

	version=draft
fi

# Create archive for each target runtime
source="$(mktemp -d)"

for runtime in $runtimes; do
    archive="$artifact-$version-$runtime.zip"

    echo >&2 "publishing $archive..."

	dotnet publish --nologo -c Release -f "$framework" -r "$runtime" -v quiet "$base/$project"

	ln -s "$(realpath "$base/$project/bin/Release/$framework/$runtime/publish")" "$source/$artifact"

	( cd "$source" && zip -qr "$archive" "$artifact" )

	mv "$source/$archive" "$base/$archive"
	rm "$source/$artifact"
done

echo >&2 "done."

# Cleanup
rm -r "$source"
