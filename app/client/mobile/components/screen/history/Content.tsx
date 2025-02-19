import { colors } from "@/constants/colors";
import useColorizer from "@/hooks/useColorizer";
import { AntDesign, Feather } from "@expo/vector-icons";
import { Dispatch, SetStateAction, useCallback, useState } from "react";
import { StyleSheet } from "react-native";
import { TouchableOpacity, View } from "react-native";
import { TextInput } from "react-native-gesture-handler";
import HistoryList from "./HistoryList";

export default function Content() {
  const [searchValue, setSearchValue] = useState<string>("");

  return (
    <View className='h-full px-4'>
      <SearchBar
        searchValue={searchValue}
        setSearchValue={setSearchValue}
        isSearching={false}
      />
      <HistoryList keyword={searchValue} />
    </View>
  );
}

type SearchBarProps = {
  searchValue: string;
  setSearchValue: Dispatch<SetStateAction<string>>;
  isSearching: boolean;
};

const SearchBar = ({ searchValue, setSearchValue, isSearching }: SearchBarProps) => {
  const { c } = useColorizer();
  const { black, white, gray } = colors;
  const [isFocused, setIsFocused] = useState(false);

  const handleSearch = useCallback((text: string) => {
    setSearchValue(text);
  }, []);

  const handleCancel = useCallback(() => {
    setSearchValue("");
  }, []);

  const styles = StyleSheet.create({
    verticallyCentered: {
      position: "absolute",
      top: "50%",
      transform: [{ translateY: "-50%" }]
    }
  });

  return (
    <View className='flex-center relative flex-row gap-2 rounded-3xl border-[0.5px] border-gray-300 px-3'>
      <Feather
        name='search'
        size={20}
        color={c(black.DEFAULT, white.DEFAULT)}
      />
      <TextInput
        value={searchValue}
        autoCapitalize='none'
        multiline={false}
        numberOfLines={1}
        className='text-black_white h-[50px] w-[86%]'
        onFocus={() => setIsFocused(true)}
        onBlur={() => setIsFocused(false)}
        onChangeText={handleSearch}
        placeholder={"Search recipe viewing history"}
        placeholderTextColor={gray[400]}
      />

      {searchValue && isSearching && (
        <View className='absolute'>
          <AntDesign
            className='animate-spin'
            name='loading2'
            size={16}
            color={c("#A9A9A9", "#6B7280")}
          />
        </View>
      )}

      {isFocused && searchValue && !isSearching && (
        <View
          className='absolute right-3 top-3.5'
          style={styles.verticallyCentered}
        >
          <TouchableOpacity
            onPress={handleCancel}
            activeOpacity={1}
          >
            <AntDesign
              name='closecircleo'
              size={16}
              color={c("#A9A9A9", "#6B7280")}
            />
          </TouchableOpacity>
        </View>
      )}
    </View>
  );
};
