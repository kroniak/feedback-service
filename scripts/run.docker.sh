#!/usr/bin/env bash
set -euo pipefail

command -v docker >/dev/null 2>&1 || {
    echo >&2 "This script requires the docker to be installed"
    exit 1
}

SCRIPT_ROOT="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

docker run -d -p 5001:5000 -v /tmp/test.api.feedback-service.kroniak.net/logs:/app/logs --name test.api.feedback-service.kroniak.net api.feedback-service.kroniak.net:latest
