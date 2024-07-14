FROM node:18.18 AS build-env

WORKDIR /App

COPY package.json package-lock.json ./
RUN npm i

COPY . ./
RUN npm run build

FROM node:18.18

WORKDIR /App

RUN npm i -g serve

COPY --from=build-env /App/dist .

EXPOSE 80

CMD serve -s splatoon-wlrate -l 80