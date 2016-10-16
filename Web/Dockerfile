FROM mono:4.2.1.102
RUN mkdir /app
COPY . /app
EXPOSE 8080
CMD ["mono", "/app/Web.exe", "-d"]