import { Loader2, Search } from "lucide-react";
import { ChangeEvent, useRef, useState } from "react";

type Props = {
  onChange: (e: ChangeEvent<HTMLInputElement>) => void;
  isLoading: boolean;
  placeholder?: string;
};

export default function SearchBar({ onChange, isLoading, placeholder }: Props) {
  const ref = useRef<HTMLInputElement>(null);
  const [isFocused, setIsFocused] = useState(false);

  return (
    <div
      className={`flex-center relative w-full max-w-lg items-center gap-2 rounded-2xl border-[1.5px] bg-primary/15 px-4 py-3 ${isFocused ? "border-primary bg-transparent" : "border-transparent"}`}
    >
    <div className="pb-1">
      <Search
        className='text-primary'
        size={20}
      />
    </div>
      <input
        ref={ref}
        type='text'
        placeholder={placeholder || "Search by username, name, gmail,..."}
        className='w-full bg-transparent text-primary outline-none placeholder:text-primary/50'
        onChange={onChange}
        onFocus={() => setIsFocused(true)}
        onBlur={() => setIsFocused(false)}
      />
      {ref.current?.value && isLoading && (
        <div className='absolute right-3 top-1/2 -translate-y-1/2'>
          <Loader2 className='size-5 animate-spin text-primary' />
        </div>
      )}
    </div>
  );
}
