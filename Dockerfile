FROM mcr.microsoft.com/dotnet/core/runtime:3.1.1-bionic

ENV PATH="${PATH}:/usr/local/bin/imagecaster/"

# Add imagecaster user, and install a generic font.
RUN useradd -ms /bin/bash imagecaster && \
    apt-get update                    && \
    apt-get install -y ttf-dejavu     && \
    rm -rf /var/lib/apt/lists/*

COPY ./src/ImageCaster/bin/Release-Q8-Docker/netcoreapp3.1/linux-x64/publish/ /usr/local/bin/imagecaster/

USER imagecaster

WORKDIR /home/imagecaster
