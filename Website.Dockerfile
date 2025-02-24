FROM node:22.13.1-slim AS build
WORKDIR /app
COPY assassins-website/ ./
RUN npm install
RUN npm install -g @angular/cli
RUN ng build --configuration production
FROM nginx:stable-alpine3.20-slim AS deploy
COPY --from=build app/dist/assassins-website/browser /usr/share/nginx/html