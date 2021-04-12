pipeline {
    agent any
    stages {
        parallel {
            stage("Build web project") {
                steps {
                    dir("web") {
                        sh "docker build . -t boulundeasv/deploy-example-web-1"
                    }
                }
            }
            stage("Deliver web project") {
                steps {
                    withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                        sh 'docker login -u ${USERNAME} -p ${PASSWORD}'
                        sh "docker push boulundeasv/deploy-example-web-1"
                    }
                }
            }
        }
        parallel {
            stage("Build API project") {
                steps {
                    dir("api") {
                        sh "dotnet build"
                        sh "docker build . -t boulundeasv/deploy-example-api-1"
                    }
                }
            }
            stage("Deliver api project") {
                steps {
                    withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                        sh 'docker login -u ${USERNAME} -p ${PASSWORD}'
                        sh "docker push boulundeasv/deploy-example-api-1"
                    }
                }
            }
        }
        stage("Release to test") {
            steps {
                sh "docker-compose -p staging -f docker-compose.yml -f docker-compose.test.yml up -d"
            }
        }
    }
}