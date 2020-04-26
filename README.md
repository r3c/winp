Winp: Windows Nginx PHP development server
==========================================

[![license](https://img.shields.io/github/license/r3c/winp.svg)](https://opensource.org/licenses/MIT)

Overview
--------

Winp is an open-source (MIT) Nginx/PHP server for Windows. It provides a
one-click portable install of required software for PHP development:

* [Nginx](https://nginx.org/)
* [PHP](https://www.php.net/)
* [MariaDB](https://mariadb.org/)
* [PhpMyAdmin](https://www.phpmyadmin.net/)

Please note Winp is a development tool and is not suitable for production
usage.


Installation
------------

Download latest [Winp release](https://github.com/r3c/winp/releases) from
GitHub or compile from source and run `Winp.exe`. Optionally click
"Configure" button to change default options then click "Install" to download
and install required software with options you previously defined. Only
portable packages will be downloaded and extracted in a sub-directory which
you can change in configuration options.

Once install is complete click "Start services" button and point your browser
to either:

* http://localhost/ to browse your local web directory
* http://localhost/phpmyadmin/ to open PhpMyAdmin

Local directories and URLs can be changed through configuration options.


Licence
-------

This project is open-source, released under MIT licence. See `LICENSE.md` file
for details.


Author
------

[Rémi Caput](http://remi.caput.fr/) (v.github.com+winp [at] mirari [dot] fr)
