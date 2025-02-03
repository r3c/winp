# Winp: Windows Nginx PHP development server

[![Build Status](https://img.shields.io/github/actions/workflow/status/r3c/winp/verify.yml?branch=master)](https://github.com/r3c/winp/actions/workflows/verify.yml)
[![license](https://img.shields.io/github/license/r3c/winp.svg)](https://opensource.org/licenses/MIT)

![image](https://github.com/r3c/winp/blob/resource/readme/service.png?raw=true)
![image](https://github.com/r3c/winp/blob/resource/readme/configuration.png?raw=true)

## Overview

Winp is an open-source (MIT) Nginx/PHP server for Windows. It provides a
one-click portable install of required software for PHP development:

- [Nginx](https://nginx.org/)
- [PHP](https://www.php.net/)
- [MariaDB](https://mariadb.org/)
- [PhpMyAdmin](https://www.phpmyadmin.net/)

Please note Winp is a development tool and is not suitable for production
usage.

## Installation

Download latest [Winp release](https://github.com/r3c/winp/releases) from
GitHub or compile from source, unpack anywhere you want then run `Winp.exe`.

- Click "Configure" button to change server bindings or locations (optional).
- Click "Start services" button to download, configure and start packages.
- Wait for a few minutes (on first start) and accept Windows network prompts.

You should see all packages in "Running" status (or "Ready" for phpMyAdmin
which doesn't execute as a background service). Click the "Open browser"
button to have your default browser open to http://localhost/ or whatever
URL you set as first location.

If you didn't change default configuration you can also navigate to
http://localhost/phpmyadmin/ to open PhpMyAdmin.

## Configuration

In the configuration panel you can tweak how required packages are installed
and run.

- `Install directory` is the directory where all packages are downloaded.
  They're all portable packages stored in their own directory, along with files
  they may create (e.g. logs, data files for MariaDB).
- `Server address` and `Port` can be changed to select which IP address and
  port Nginx will listen on.
- `Locations` is a list of Nginx locations and how Nginx should process HTTP
  requests for each of them.

For each location you must specify a base URL (plain text only, no support for
regexps yet) and a type that defines which configuration preset must be applied
for this location. Available location types are:

- `No access (HTTP 403)`: reject all requests with HTTP 403 error.
- `Execute PHP files by URL`: search for PHP file by concatenating base
  directory and URL path, and execute that file to produce HTTP response.
- `Pass all requests to index.php`: always execute `index.php` file from base
  directory to produce HTTP response.
- `Static files only`: serve static files (no PHP support).
- `Use PhpMyAdmin`: execute PhpMyAdmin package.

For more advanced configuration, you'll find `*.template` files created by Winp
in package directories (e.g. `nginx/$version/conf/nginx.conf.template`). These
files are used to produce actual configuration files each time services are
started, so you can tweak them to your needs. They're created if missing and
reused otherwise, and are written using [Cottle](https://r3c.github.io/cottle/)
template engine with
[custom delimiters](https://cottle.readthedocs.io/en/stable/page/04-configuration.html#delimiters-customization)
set to "{{", "{|}" and "}}".

## Custom packages

You can replace the default versions for all packages by modifying the
`Winp.json` file that gets created next to `Winp.exe` after you first save some
configuration change within the application.

Locate the `userVariants` field for the package you want to customize and set it
to any non-empty array to replace application defaults. Each item within this
array is expected to contain the following fields:

- `downloadUrl` is the full download URL of a ZIP version of the package
- `identifier` is a human-readable unique identifier for the version
- `pathInArchive` is a relative path to package's directory within archive

Here is an example of user variant for Nginx:

```
{
    "downloadUrl": "https://nginx.org/download/nginx-1.27.3.zip",
    "identifier": "1.27.3",
    "pathInArchive": "nginx-1.27.3"
}
```

## Credits

- [Freepik](https://www.flaticon.com/fr/auteurs/freepik) for the nice Elephant icon

## Resource

- Contact: v.github.com+winp [at] mirari [dot] fr
- License: [license.md](license.md)
