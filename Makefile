SHELL := /bin/bash

export GBFORTH_PATH := $(shell pwd)/lib

LIB_FILES=lib/*.fs lib/core/*.fs
SOURCE_FILES=gbforth src/*.fs src/utils/*.fs src/compiler/*.fs shared/*.fs

TEST_FILES = $(wildcard test/*.fs) $(wildcard test/*/*.fs)
TEST_OBJS = $(subst .fs,.gb,$(TEST_FILES))

GBFORTH = ./gbforth

.PHONY: all examples tests

# Pattern rule to build gbforth roms
%.gb: %.fs $(SOURCE_FILES) $(LIB_FILES)
	$(GBFORTH) --pad-ff $< $@

all: examples

#
# Examples
#
examples: \
	examples/hello-world-asm/hello.gb \
	examples/hello-world/hello.gb \
	examples/sokoban/sokoban.gb

examples/hello-world-asm/hello.gb: examples/hello-world-asm/hello.fs examples/hello-world-asm/*.fs $(SOURCE_FILES) $(LIB_FILES)
	$(GBFORTH) --no-kernel $< $@
	@cd examples/hello-world-asm/ && shasum -c hello.gb.sha

#
# Tests
#
check: tests examples
	gforth src/asm.spec.fs -e bye
	( cd test/; yarn test )

tests: $(TEST_OBJS)

clean:
	-rm -f examples/hello-world-asm/hello.gb
	-rm -f examples/hello-world/hello.gb
	-rm -f examples/sokoban/sokoban.gb
	-rm -f $(TEST_OBJS)

#
# Docker commands
#
docker-build:
	docker build -t amshackers/gbforth .
