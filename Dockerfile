FROM mcr.microsoft.com/dotnet/core/runtime:3.1.0-alpine3.10
RUN adduser -u 1001 -Sh /home/imagecaster imagecaster                                 && \
    apk add --no-cache --update imagemagick=7.0.8.68-r0 ttf-dejavu=2.37-r1 zip=3.0-r7
USER imagecaster
WORKDIR /home/imagecaster
