pipeline {
    agent {
        docker {
            image 'gableroux/unity3d:2019.1.8f1-windows-add-2017-4-29f1-2018-4-3f1-2019-1-8f1'
            args '-u 0 --group-add audio'
        }
    }

    environment {
        BUILD_NAME = 'JenkinsBuild'
        UNITY_LICENSE_FILE = credentials('jenkins-unity-license-2019')
    }

    stages {
        stage('Setup') {
            steps {
                sh 'chmod +x ./ci/before_script.sh && ./ci/before_script.sh'
            }
        }


        stage('Edit Mode Test') {
            environment {
                TEST_PLATFORM='editmode'
            }

            steps {
                sh 'chmod +x ./ci/test.sh && ./ci/test.sh'
            }
        }

        stage('Play Mode Test') {
            environment {
                TEST_PLATFORM='playmode'
            }

            steps {
                sh 'chmod +x ./ci/test.sh && ./ci/test.sh'
            }
        }

        stage('Windows Build') {
            environment {
                BUILD_TARGET='StandaloneWindows64'
            }

            steps {
                sh 'chmod +x ./ci/build.sh && ./ci/build.sh'
            }
        }
    }
}
