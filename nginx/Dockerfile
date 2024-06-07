#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM nginx:alpine as base
COPY nginx/proxy.conf /etc/nginx/proxy.conf
EXPOSE 443
EXPOSE 80

FROM base as dev
COPY nginx/nginx.conf /etc/nginx/nginx.conf


FROM base as prod
COPY nginx/nginx.production.conf /etc/nginx/nginx.conf