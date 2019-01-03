FROM oldmankris/alpine-gforth

WORKDIR /data

COPY gbforth /data
COPY src /data/src/
COPY lib /data/lib/
COPY shared /data/shared

ENV GBFORTH_PATH /data/lib

ENTRYPOINT ["./gbforth"]
