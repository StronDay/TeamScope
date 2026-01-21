#!/bin/sh

echo "Генерация..."
# 1. Генерация
cd /workspaces/TeamScope/doc/doxygen
doxygen doxy_config 2>/dev/null || (doxygen -g && doxygen doxy_config)