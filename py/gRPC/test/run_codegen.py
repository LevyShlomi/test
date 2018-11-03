from grpc_tools import protoc
import os
 
#for f in os.listdir('./protos'):
protoc.main((
    '',
    '-I./protos',
    '--python_out=./src',
    '--grpc_python_out=./src',
    './protos/all.proto',
))

#region Install Pre-requisites

##requires intallation of grpc and proto tools =>  
# python -m pip install grpcio 
# python -m pip install grpcio-tools googleapis-common-protos

#endregion
