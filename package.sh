#!/bin/sh -e

base="$(dirname "$0")"
root=winp

# Retreive latest version from current HEAD
version="$(git --work-tree "$base" tag --points-at HEAD)"

if [ -z "$version" ]; then
	echo >&2 "warning: current HEAD doesn't point to a tag"

	version=.draft
fi

# Create archive for each target runtime
framework=netcoreapp3.1
source="$(mktemp -d)"

for runtime in win-x64; do
	dotnet publish -c Release -r "$runtime" -v quiet "$base/Winp"

	ln -s "$(realpath "$base/Winp/bin/Release/$framework/$runtime/publish")" "$source/$root"

	archive="$root-v$version-$runtime.zip"

	( cd "$source" && zip -qr "$archive" "$root" )

	mv "$source/$archive" "$base/$archive"
	rm "$source/$root"
done

# Cleanup
rm -r "$source"
