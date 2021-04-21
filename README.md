# AliyunDDNS
 aliyun dynamic DNS, dotnet 5.0, run at docker

# docker
* ~/aliyun.ddns.config.json
```json
{
    // aliyun dynamic dns config file
    // ~/aliyun.ddns.config.json
    // docker:  ~/ -> /app/

    "interval": 10, //seconds
    "showIPUrl": "https://url.anycore.cn/ip", //out my ip address text
    "accessKeys": { //accessKey map
        "default": {
            "accessKeyId": "your accessKey ID",
            "accessKeySecret": "your accessKey Secret"
        }
    },
    "domains": { // domain map
        "mydomain.com": {
            "provider": "aliyun", //dns provider
            "accessKey": "default", //refence accessKey map
            "items": [
                {
                    "type": "A",
                    "name": "home",
                    "ttl": 100
                }
            ]
        }
    }
}

```

