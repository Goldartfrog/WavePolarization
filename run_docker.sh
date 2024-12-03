#!/usr/bin/env bash

export UNITY_VERSION="2018.2.3f1"
docker run -it --rm \
    -e "UNITY_USERNAME=josephk4+unityciserver@illinois.edu" \
    -e "UNITY_PASSWORD=Password1" \
    -e "TEST_PLATFORM=linux" \
    -e "WORKDIR=/root/project" \
    -v "$(pwd):/root/project" \
    gableroux/unity3d:$UNITY_VERSION \
    bash
