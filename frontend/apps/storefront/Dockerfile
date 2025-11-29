FROM node:20-alpine AS build
WORKDIR /app

COPY package.json package-lock.json* ./
COPY apps ./apps
COPY packages ./packages
COPY tsconfig.json ./tsconfig.json
RUN npm install
RUN npm run build:storefront

FROM nginx:1.27-alpine
COPY --from=build /app/apps/storefront/dist /usr/share/nginx/html
EXPOSE 4173
CMD ["nginx", "-g", "daemon off;"]
