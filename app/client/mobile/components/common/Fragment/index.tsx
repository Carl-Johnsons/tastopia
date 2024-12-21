import React, { Fragment, ReactNode } from "react";

interface FragmentProps {
  key?: string | number;
  children: ReactNode;
}

const Fragment = ({ key, children }: FragmentProps) => {
  return <Fragment key={key}>{children}</Fragment>;
};

export default Fragment;
