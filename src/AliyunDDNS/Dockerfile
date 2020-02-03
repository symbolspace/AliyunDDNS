FROM mcr.microsoft.com/dotnet/core/runtime:3.1
MAINTAINER symbolspace<symbolspace@outlook.com>

LABEL description="AliyunDDNS"
LABEL version="1.0"

WORKDIR /app
COPY . .

RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

VOLUME /app

ENTRYPOINT ["dotnet", "AliyunDDNS.exe"]