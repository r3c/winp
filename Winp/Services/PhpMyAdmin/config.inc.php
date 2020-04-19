<?php

declare(strict_types=1);

// Encryption
$cfg['blowfish_secret'] = '
{{set alpha to "abcdefghijklmnopqrstuvwxyz"}}
{{set digit to "0123456789"}}
{{set pool to cat(lcase(alpha), ucase(alpha), digit)}}
{{for i in range(32):
    {{slice(pool, rand(len(pool)), 1)}}
}}
';

// Servers
$i = 0;
$i++;

$cfg['Servers'][$i]['auth_type'] = 'config';
$cfg['Servers'][$i]['host'] = 'localhost';
$cfg['Servers'][$i]['user'] = 'root';
$cfg['Servers'][$i]['password'] = '';
$cfg['Servers'][$i]['compress'] = false;
$cfg['Servers'][$i]['AllowNoPassword'] = true;

$cfg['ServerDefault'] = $i;
