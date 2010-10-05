$(if $(wildcard config.make),,$(error You need to run './configure' before running 'make'.))
include config.make
conf=Debug
SLN=src/libev-sharp/libev-sharp.sln
VERBOSITY=normal
version=0.0.2
install_bin_dir = "$(prefix)/lib/libev-sharp/"
install_pc_dir = "$(prefix)/lib/pkgconfig/"
distdir = "libev-sharp-$(version)"

XBUILD_ARGS=/verbosity:$(VERBOSITY) /nologo

srcdir_abs=$(shell pwd)
LOCAL_CONFIG=$(srcdir_abs)/../../local-config

ifeq ($(strip $(wildcard "${LOCAL_CONFIG}/monodevelop.pc")),)
	XBUILD=PKG_CONFIG_PATH="${LOCAL_CONFIG}:${PKG_CONFIG_PATH}" xbuild $(XBUILD_ARGS)
else
	XBUILD=xbuild $(XBUILD_ARGS)
endif

NUNIT_CONSOLE = nunit-console4

define LIBEV_SHARP_PC_SCRIPT
Name: libev-sharp
Description: Managed wrapper for the libev library.
Version: $(version)

Requires: 
Libs: -r:$(install_bin_dir)/libev-sharp.dll
endef
export LIBEV_SHARP_PC_SCRIPT


all: 
	@test -f config.make || (echo "You need to run ./configure." && exit 1)
	$(XBUILD) $(SLN) /property:Configuration=$(conf)
	echo "$$LIBEV_SHARP_PC_SCRIPT" > build/libev-sharp.pc

clean:
	$(XBUILD) $(SLN) /property:Configuration=$(conf) /t:Clean
	rm -rf build/*

install: install-bin install-pkg-config

install-bin: all
	test -d "$(install_bin_dir)" || mkdir "$(install_bin_dir)"
	cp -rf ./build/* "$(install_bin_dir)"

install-pkg-config:
	test -d "$(install_pc_dir)" || mkdir "$(install_pc_dir)"
	echo "$$LIBEV_SHARP_PC_SCRIPT" > $(install_pc_dir)libev-sharp.pc

uninstall:
	rm -rf "$(installdir)"

dist: clean
	rm -rf "$(distdir)"
	mkdir "$(distdir)"
	cp -rf ./src/ "$(distdir)"
	cp -rf configure Makefile "$(distdir)"
	tar cjvf libev-sharp-"$(version)".tar.bz2 libev-sharp-"$(version)"

