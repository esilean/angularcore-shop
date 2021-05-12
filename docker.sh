docker run -d --name angmysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root mysql:8.0.20
docker run -d --name angredis redis