import React from 'react'

const Status = ({isActive}: {isActive: boolean}) => {
  return isActive ? (
    <span className="flex items-center gap-1 text-sm text-green-500">
      <span className="size-2 rounded-full bg-green-500"></span>
      Active
    </span>
  ) : (
    <span className="flex items-center gap-1 text-sm text-red-500">
      <span className="size-2 rounded-full bg-red-500"></span>
      Inactive
  </span>
  )
}

export default Status