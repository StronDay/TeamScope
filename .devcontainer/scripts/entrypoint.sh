#!/bin/sh
echo "🚀 Старт"

echo "Генерация..."
# 1. Генерация
cd /workspaces/TeamScope/doc/doxygen
doxygen doxy_config 2>/dev/null || (doxygen -g && doxygen doxy_config)

echo "Зпуск скрипта"
# 2. Сервер в фоне
cd /workspaces/TeamScope/doc/doxygen/doxy_data/html
python3 -m http.server 8000 &

# 3. Бесконечное ожидание
exec sleep infinity