FROM mcr.microsoft.com/dotnet/core/runtime:3.1.0-alpine3.10

RUN adduser -u 1001 -Sh /home/imagecaster imagecaster && \
    apk add --no-cache --update ttf-dejavu=2.37-r1

USER imagecaster

WORKDIR /home/imagecaster
