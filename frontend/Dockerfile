FROM node:14.17.3-alpine3.14 as builder
COPY package.json yarn.lock ./
RUN yarn && mkdir /client && mv ./node_modules ./client
WORKDIR /client
COPY . .
RUN yarn build

FROM nginx:1.21.1-alpine
COPY nginx.conf /etc/nginx/nginx.conf
RUN rm -rf /usr/share/nginx/html/*
COPY --from=builder /client/dist/* /usr/share/nginx/html
EXPOSE 80 443
ENTRYPOINT ["nginx", "-g", "daemon off;"]
