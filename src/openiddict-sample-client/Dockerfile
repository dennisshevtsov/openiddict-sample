FROM node:lts AS build
WORKDIR /app
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.24.0
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/openiddict-sample-client /usr/share/nginx/html
