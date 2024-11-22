FROM node:slim AS build
WORKDIR /app
COPY assassins-website/ ./
RUN npm install
RUN npm install -g @angular/cli
RUN ng build
FROM nginx:1.27.2-alpine-slim AS deploy
COPY --from=build app/dist/assassins-website/browser /usr/share/nginx/html
EXPOSE 80