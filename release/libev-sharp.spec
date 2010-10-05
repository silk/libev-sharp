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
BuildRequires:  mono-devel >= 2.6
Source0:        %{name}-%{version}.tar.bz2
Summary:        A Managed wrapper around the libev event processing library
Group:          Programming
BuildArch:      noarch

%description
libev-sharp is a managed wrapper around the libev library.

%files
%defattr(-, root, root)
%{_prefix}/lib/%{name}
%{_bindir}/%{name}

%prep
%setup -q -n %{name}-%{version}


%build
./configure --prefix=%{prefix}
make

%install
make install

%clean
rm -rf %{buildroot}

%changelog