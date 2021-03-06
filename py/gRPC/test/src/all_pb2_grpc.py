# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
import grpc

import all_pb2 as all__pb2


class TesterStub(object):
  # missing associated documentation comment in .proto file
  pass

  def __init__(self, channel):
    """Constructor.

    Args:
      channel: A grpc.Channel.
    """
    self.SaveSnapshot = channel.unary_unary(
        '/activity.Tester/SaveSnapshot',
        request_serializer=all__pb2.Dataset.SerializeToString,
        response_deserializer=all__pb2.Empty.FromString,
        )
    self.Echo = channel.unary_unary(
        '/activity.Tester/Echo',
        request_serializer=all__pb2.Record.SerializeToString,
        response_deserializer=all__pb2.Record.FromString,
        )


class TesterServicer(object):
  # missing associated documentation comment in .proto file
  pass

  def SaveSnapshot(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')

  def Echo(self, request, context):
    # missing associated documentation comment in .proto file
    pass
    context.set_code(grpc.StatusCode.UNIMPLEMENTED)
    context.set_details('Method not implemented!')
    raise NotImplementedError('Method not implemented!')


def add_TesterServicer_to_server(servicer, server):
  rpc_method_handlers = {
      'SaveSnapshot': grpc.unary_unary_rpc_method_handler(
          servicer.SaveSnapshot,
          request_deserializer=all__pb2.Dataset.FromString,
          response_serializer=all__pb2.Empty.SerializeToString,
      ),
      'Echo': grpc.unary_unary_rpc_method_handler(
          servicer.Echo,
          request_deserializer=all__pb2.Record.FromString,
          response_serializer=all__pb2.Record.SerializeToString,
      ),
  }
  generic_handler = grpc.method_handlers_generic_handler(
      'activity.Tester', rpc_method_handlers)
  server.add_generic_rpc_handlers((generic_handler,))
