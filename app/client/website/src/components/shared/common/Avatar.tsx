import {
  AvatarFallback,
  AvatarImage,
  Avatar as BaseAvatar
} from "@/components/ui/avatar";
import { AvatarProps } from "@radix-ui/react-avatar";

type Props = AvatarProps & {
  src: string;
  alt: string;
};

export default function Avatar({ src, alt, className, ...props }: Props) {
  return (
    <BaseAvatar
      className={`min-w-fit cursor-pointer ${className}`}
      {...props}
    >
      <AvatarImage
        className='bg-orange-100'
        src={src}
      />
      <AvatarFallback className='bg-orange-100'>{alt ? alt.at(0) : ""}</AvatarFallback>
    </BaseAvatar>
  );
}
