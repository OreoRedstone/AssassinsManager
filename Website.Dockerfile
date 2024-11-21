FROM node AS build
WORKDIR /app
COPY assassins-website/package*.json ./
RUN npm install
RUN npm install -g @angular/cli
COPY assassins-website/ ./
RUN ng build --configuration=production
FROM nginx AS deploy
COPY --from=build app/dist/assassins-website /usr/share/nginx/html
EXPOSE 80