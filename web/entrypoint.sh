#!/bin/bash

cd js
cd configuration
ls -li
cp js/configuration/config.$EnvironmentName.js js/configuration/config.js # 2> /dev/null || :
nginx -g 'daemon off;'