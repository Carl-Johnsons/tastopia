import {
  AvatarFallback,
  AvatarImage,
  Avatar as BaseAvatar
} from "@/components/ui/avatar";
import { AvatarProps } from "@radix-ui/react-avatar";

type Props = {
  src: string;
  alt: string;
} & Pick<AvatarProps, "className">;

export default function Avatar({ src, alt, className }: Props) {
  return (
    <BaseAvatar className={`min-w-fit cursor-pointer ${className}`}>
      <AvatarImage
        className='bg-orange-100'
        src={src}
      />
      <AvatarFallback className='text-white_black bg-orange-100'>
        {alt.at(0)}
      </AvatarFallback>
    </BaseAvatar>
  );
}
