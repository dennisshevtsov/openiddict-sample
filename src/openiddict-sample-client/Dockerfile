FROM node:lts AS build
WORKDIR /app
COPY ["./src/openiddict-sample-client/package*.json", "./"]
RUN npm ci
COPY ["./src/openiddict-sample-client/.", "./"]
RUN npm run build

FROM nginx:1.24.0 AS openiddict-client
COPY ["./src/openiddict-sample-client/nginx.conf", "/etc/nginx/nginx.conf"]
#COPY ["./src/openiddict-sample-client/nginx.conf", "/etc/nginx/conf.d/default.conf"]
COPY --from=build /app/dist/openiddict-sample-client /usr/share/nginx/html
