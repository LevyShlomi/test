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
