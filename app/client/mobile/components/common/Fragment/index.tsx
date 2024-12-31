import React, { Fragment as ReactFragment, ReactNode } from "react";

interface FragmentProps {
  key?: string | number;
  children: ReactNode;
}

const Fragment = ({ key, children }: FragmentProps) => {
  return <ReactFragment key={key}>{children}</ReactFragment>;
};

export default Fragment;
