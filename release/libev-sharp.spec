#
# spec file for package libev-sharp
#
# Copyright (c) 2010 Jackson Harper (jacksonh@gmail.com)
#
#

Name:           libev-sharp
Version:        0.0.1
Release:        1
License:        MIT/X11
BuildRoot:      %{_tmppath}/%{name}-%{version}-build
BuildRequires:  mono-devel
BuildRequires:  libev-devel
Source0:        libev-sharp-%{version}.tar.bz2
Source1:        %{name}-rpmlintrc
Summary:        A Managed wrapper around the libev event processing library
Group:          Development/Libraries/Other
BuildArch:      noarch

%description
libev-sharp is a managed wrapper around the libev event processing library. This library allows you to watch for and respond to IO events and Timer events.

%files
%defattr(-, root, root)
%{_prefix}/lib/%{name}
%{_prefix}/share/pkgconfig/%{name}.pc

%prep
%setup -q -n %{name}-%{version}


%build
./configure
make

%install
install -d %{buildroot}%{_prefix}/lib/%{name}
for i in build/*.dll*; do  
 install -c -m 644 $i %{buildroot}%{_prefix}/lib/%{name}/
done
install -d %{buildroot}%{_prefix}/share/pkgconfig/
install -c -m 644 build/libev-sharp.pc %{buildroot}%{_prefix}/share/pkgconfig/%{name}.pc


%clean
rm -rf %{buildroot}

%changelog
