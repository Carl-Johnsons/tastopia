import { useEffect, useState } from "react";
import { transformPlatformURI } from "../utils/functions";

const CONSUL_BASE_URL = transformPlatformURI("http://localhost:8500");

type ConsulServiceResponse = {
  Node: {
    ID: string;
    Node: string;
    Address: string;
  };
  Service: {
    ID: string;
    Service: string;
    Tags: string[];
    Address: string[];
    Port: number;
  };
  Checks: [
    {
      Node: string;
      CheckID: string;
      Name: string;
      Status: string;
      Notes: string;
      Output: string;
    }
  ];
};

type Props = {
  serviceName: string;
};

/**
 * get the service uri from service discovery by selecting the first instance
 * 
 * TODO: implement round-robin if there are 2 or more instances
 * @param serviceName 
 * @returns service uri or undefined
 */
const fetchServiceUri = async (serviceName: string) => {
  const response = await fetch(
    `${CONSUL_BASE_URL}/v1/health/service/${serviceName}?passing=true`
  );
  const services: ConsulServiceResponse[] = await response.json();
  if (services.length == 0) {
    console.error(`No healthy instances found for service: ${serviceName}`);
    return;
  }
  const service = services[0].Service;
  const address = service.Address || services[0].Node.Address;
  const port = service.Port;
  const isSecure = service.Tags.includes("secure=true");
  const scheme = isSecure ? "https" : "http";
  const uri = transformPlatformURI(`${scheme}://${address}:${port}`);

  console.log("Service query successful with URI", uri);
  return uri;
};

const useServiceDiscovery = ({ serviceName }: Props): string => {
  const [serviceUri, setServiceUri] = useState<string>("");

  useEffect(() => {
    const getServiceUri = async () => {
      const uri = await fetchServiceUri(serviceName);
      if (uri) setServiceUri(uri);
    };
    getServiceUri();
  }, []);

  return serviceUri;
};

export { useServiceDiscovery, fetchServiceUri };
