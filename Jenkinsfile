pipeline {
    agent any
    stages {
        // BAD APPROACH: We can't assure that we build before we deliver!
        // stage("Build + Deliver web") {
        //     steps {
        //         parallel(
        //             build: {
        //                 dir("web") {
        //                     sh "docker build . -t boulundeasv/deploy-example-web-1"
        //                 }
        //             },
        //             deliver: {
        //                 withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
        //                     sh 'docker login -u ${USERNAME} -p ${PASSWORD}'
        //                     sh "docker push boulundeasv/deploy-example-web-1"
        //                 }
        //             }
        //         )
        //     }
        // }
        // stage("Build + Deliver API") {
        //     steps {
        //         parallel(
        //             build: {
        //                 dir("api") {
        //                     sh "dotnet build"
        //                     sh "docker build . -t boulundeasv/deploy-example-api-1"
        //                 }
        //             },
        //             deliver: {
        //                 withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
        //                     sh 'docker login -u ${USERNAME} -p ${PASSWORD}'
        //                     sh "docker push boulundeasv/deploy-example-api-1"
        //                 }
        //             }
        //         )
        //     }
        // }
        // BETTER APPROACH:
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
        // BAD APPROACH: We want a manual step making sure that the acceptance test has been conducted
        // stage("Release to production") {

        // GOOD, BUT: This helps us introduct a manual step, but it blocks an executor until we're ready
        //     // input { 
        //     //     message "Release to production?"
        //     // }
        //     steps {
        //         sh "docker-compose -p production -f docker-compose.yml -f docker-compose.prod.yml up -d"
        //     }
        // }

        // GOOD: Introduce another Jenkins job to take care of the production part
    }
}