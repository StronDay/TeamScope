FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0-bookworm

RUN apt-get update && apt-get -y install --no-install-recommends \
    doxygen \
    doxygen-doc \
    graphviz \
    plantuml \
    default-jre-headless \
    clang \
    clang-format \
    make \
    git \
    wget \
    curl \
    python3 \
    python3-pip \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /workspaces/Api

CMD ["dotnet", "run"]