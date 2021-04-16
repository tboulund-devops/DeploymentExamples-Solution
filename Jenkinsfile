pipeline {
    agent any
    stages {
        stage("Build") {
            steps {
                parallel(
                    web: {
                        dir("web") {
                            sh "docker build . -t boulundeasv/deploy-example-web-1"
                        }
                    },
                    api: {
                        dir("api") {
                            sh "dotnet build"
                            sh "docker build . -t boulundeasv/deploy-example-api-1"
                        }
                    }
                )
            }
        }
        stage("Deliver") {
            steps {
                parallel(
                    web: {
                        withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                            sh 'docker login -u ${USERNAME} -p ${PASSWORD}'
                            sh "docker push boulundeasv/deploy-example-web-1"
                        }
                    },
                    api: {
                        withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                            sh 'docker login -u ${USERNAME} -p ${PASSWORD}'
                            sh "docker push boulundeasv/deploy-example-api-1"
                        }
                    }
                )
            }
        }
        stage("Release to test") {
            steps {
                sh "docker-compose -p staging -f docker-compose.yml -f docker-compose.test.yml up -d"
            }
        }
    }
}