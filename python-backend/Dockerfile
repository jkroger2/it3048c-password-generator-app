FROM python:3.13-slim-bullseye

WORKDIR /usr/src

COPY . .

ENV PYTHONPATH=/usr/src

RUN pip install --no-cache-dir -r requirements.txt

EXPOSE 5000

