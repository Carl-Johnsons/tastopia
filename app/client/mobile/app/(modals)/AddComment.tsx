import { selectUser } from "@/slices/user.slice";
import { useCallback, useEffect, useState } from "react";
import {
  KeyboardAvoidingView,
  SafeAreaView,
  StatusBar,
  Text,
  TextInput,
  View
} from "react-native";
import Entypo from "@expo/vector-icons/Entypo";
import { useKeyboard } from "@/hooks/useKeyboard";
import { Easing, useSharedValue, withTiming } from "react-native-reanimated";
import { ScrollView } from "react-native";
import { router } from "expo-router";
import useColorizer from "@/hooks/useColorizer";
import { colors } from "@/constants/colors";

const AddComment = () => {
  const { c } = useColorizer();
  const { black, white } = colors;

  const user = selectUser();
  const [text, setText] = useState<string>("");
  const { keyboardHeight } = useKeyboard();
  const keyboardOffset = useSharedValue(keyboardHeight * -1);
  const [isClosing, setIsClosing] = useState<boolean>(false);

  const backgroundColor = "#FFFFFF";
  const placeholderColor = "text-gray-500";

  const animate = useCallback(() => {
    keyboardOffset.value = withTiming(keyboardHeight * -1, {
      duration: 500,
      easing: Easing.bezier(0, 0.6, 0.3, 1)
    });
  }, [keyboardHeight]);

  useEffect(() => {
    animate();
  }, [keyboardHeight]);

  const clearInput = () => {
    setText("");
  };

  return (
    <SafeAreaView style={{ backgroundColor }}>
      <View
        className='h-full w-full flex-col px-3'
        style={{ backgroundColor }}
      >
        <View className='h-[60px] flex-row items-center justify-between px-3'>
          <Text
            style={{ color: c(black.DEFAULT, white.DEFAULT) }}
            onPress={() => (text.length > 0 ? setIsClosing(true) : router.back())}
          >
            Cancel
          </Text>
          <Text
            style={{ color: c(black.DEFAULT, white.DEFAULT) }}
            className='font-bold'
          >
            Add Comment
          </Text>
          <Text
            style={{ color: c(black.DEFAULT, white.DEFAULT) }}
            onPress={() => (text.length > 0 ? setIsClosing(true) : router.back())}
          >
            Comment
          </Text>
        </View>

        <View
          style={{ borderColor: "#A9A9A9" }}
          className='border border-gray-500/20'
        />
        <KeyboardAvoidingView>
          <ScrollView>
            <View className='mt-5 flex-row items-start gap-3'>
              <View className='flex-1 pb-3'>
                <View className='flex-row justify-between'>
                  <Text
                    style={{ color: c(black.DEFAULT, white.DEFAULT) }}
                    className='font-bold'
                  >
                    {user.accountUsername}
                  </Text>
                  {text.length > 0 && (
                    <Entypo
                      name='cross'
                      size={20}
                      color={"lightgray"}
                      onPress={clearInput}
                    />
                  )}
                </View>
                <TextInput
                  placeholder="What's new?"
                  placeholderTextColor={placeholderColor}
                  multiline
                  value={text}
                  autoFocus={true}
                  onChangeText={setText}
                  style={{ color: c(black.DEFAULT, white.DEFAULT) }}
                  maxLength={500}
                />
              </View>
            </View>
          </ScrollView>
        </KeyboardAvoidingView>
      </View>
    </SafeAreaView>
  );
};

export default AddComment;
