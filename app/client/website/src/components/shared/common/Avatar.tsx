import {
  AvatarFallback,
  AvatarImage,
  Avatar as BaseAvatar
} from "@/components/ui/avatar";

type Props = {
  src: string;
  alt: string;
};

export default function Avatar({ src, alt }: Props) {
  return (
    <BaseAvatar className='min-w-fit cursor-pointer'>
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
