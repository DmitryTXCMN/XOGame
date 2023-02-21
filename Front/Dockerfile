FROM node as dev
EXPOSE 3000
WORKDIR /app

COPY ["Front/package.json", "/"]
COPY ["Front/package-lock.json", "/"]

RUN npm install

COPY ["Front/", "/"]

CMD ["npm", "start"]