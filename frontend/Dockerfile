#----------------------------
FROM node:19-alpine AS base
ENV PATH /app/node_modules/.bin:$PATH
WORKDIR /app
COPY package*.json ./
#----------------------------
FROM base AS dev
RUN npm init --yes
RUN npm i
COPY ./frontend .
CMD ["npm", "start"]
#-----------------------------
FROM base AS production
ENV NODE_ENV production
RUN npm ci --only=production
USER node
COPY . .
EXPOSE 3000
CMD ["npm", "run", "build"]
