import React, { Fragment, ReactNode } from "react";

interface PagoFragmentProps {
  key?: string | number;
  children: ReactNode;
}

const PagoFragment = ({ key, children }: PagoFragmentProps) => {
  return <Fragment key={key}>{children}</Fragment>;
};

export default PagoFragment;
