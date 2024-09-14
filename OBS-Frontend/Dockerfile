FROM node:20.16 AS build-env

WORKDIR /App
COPY package.json package-lock.json ./
RUN npm i
COPY . ./
RUN npm run build
# -------------
FROM node:20.16

WORKDIR /App
RUN npm i -g serve
COPY --from=build-env /App/dist .

EXPOSE 80
CMD serve -l 80
