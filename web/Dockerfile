FROM nginx

WORKDIR /usr/share/nginx/html
COPY . .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT [ "./entrypoint.sh" ]