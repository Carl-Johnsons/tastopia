"use client"
import React from 'react'
import { useParams, useRouter } from "next/navigation";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from "@/components/ui/avatar"

const Profile = () => {
  const router = useRouter();
  const handleClick = () => {

    router.push("/users/bb06e4ec-f371-45d5-804e-22c65c77f67d");
  };
  return (
    <Avatar onClick={handleClick} className='cursor-pointer'>
    <AvatarImage src="https://github.com/shadcn.png" alt="User Avatar" />
    <AvatarFallback>A</AvatarFallback>
  </Avatar>
  )
}

export default Profile