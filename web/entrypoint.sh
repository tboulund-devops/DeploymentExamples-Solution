#!/bin/bash

cp /usr/share/nginx/html/js/configuration/config.$EnvironmentName.js /usr/share/nginx/html/js/configuration/config.js 2> /dev/null || :
nginx -g 'daemon off;'