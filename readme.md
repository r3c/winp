# Winp: Windows Nginx PHP development server

[![Build Status](https://img.shields.io/github/actions/workflow/status/r3c/winp/verify.yml?branch=master)](https://github.com/r3c/winp/actions/workflows/verify.yml)
[![license](https://img.shields.io/github/license/r3c/winp.svg)](https://opensource.org/licenses/MIT)

![image](https://user-images.githubusercontent.com/979446/146273057-3ba16417-a2cc-44b5-aa68-c6a570e97049.png)
![image](https://user-images.githubusercontent.com/979446/146273072-fc8b4859-94c5-458a-9195-c3081c0d2dd5.png)

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

- Click "Install" button to download required packages ;
- Click "Configure" button to change server bindings or locations (optional) ;
- Click "Start services" button and accept Windows network prompts if need be.

You should see all packages in "Running" status (or "Ready" for phpMyAdmin
which doesn't execute as a background service). A new browser windows will also
open to http://localhost/ or whatever URL you set as first location.

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

## Credits

- [Freepik](https://www.flaticon.com/fr/auteurs/freepik) for the nice Elephant icon
- 
## Resource

- Contact: v.github.com+winp [at] mirari [dot] fr
- License: [license.md](license.md)
